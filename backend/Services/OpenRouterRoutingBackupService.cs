using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Patient_Grievance_and_Complaint_Resolution.DTOs.AiInputsandOutputs;

namespace Patient_Grievance_and_Complaint_Resolution.Services
{
    public class OpenRouterRoutingBackupService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _endpointUrl;
        private readonly string _configFilePath;
        private readonly string _outputFilePath;
        private readonly List<InvestigatorDto> _investigatorDto;

        public OpenRouterRoutingBackupService(HttpClient httpClient, string configFilePath, string outputPath, List<InvestigatorDto> investigatorDto)
        {
            _httpClient = httpClient;
            _configFilePath = configFilePath;
            _outputFilePath = outputPath;
            _investigatorDto = investigatorDto;
            _apiKey = Environment.GetEnvironmentVariable("API_KEY", EnvironmentVariableTarget.Process)
                   ?? Environment.GetEnvironmentVariable("API_KEY", EnvironmentVariableTarget.User)
                   ?? Environment.GetEnvironmentVariable("API_KEY", EnvironmentVariableTarget.Machine);

            if (string.IsNullOrWhiteSpace(_apiKey))
            {
                throw new InvalidOperationException(
                    "OpenRouter API Key not found in environment variables.");
            }

            _endpointUrl = "https://openrouter.ai/api/v1/chat/completions";
        }

        public async Task<RoutingResult> RouteIssueWithExternalConfigAsync(
            string patientChosenCategory,
            string patientIssue)
        {
            if (!File.Exists(_configFilePath))
            {
                throw new FileNotFoundException(
                    $"Configuration file not found at: {_configFilePath}");
            }
            if (!File.Exists(_outputFilePath))
            {
                throw new FileNotFoundException(
                    $"Configuration file not found at: {_outputFilePath}");
            }

            string systemInstructionText =
                await File.ReadAllTextAsync(_configFilePath);

            string inputFile =
                await File.ReadAllTextAsync(_outputFilePath);
            var investigatorJson = JsonSerializer.Serialize(_investigatorDto);

            var requestPayload = new
            {
                model = "openai/gpt-oss-20b:free",

                response_format = new
                {
                    type = "json_object"
                },

                messages = new object[]
                {
                    new
                    {
                        role = "system",
                        content = systemInstructionText + inputFile+investigatorJson,
                    },
                    new
                    {
                        role = "user",
                        content =
                            $"Patient Selected Category: {patientChosenCategory}\n" +
                            $"Patient Issue: {patientIssue}"
                    }
                }
            };

            string jsonPayload = JsonSerializer.Serialize(requestPayload);

            using var request =
                new HttpRequestMessage(HttpMethod.Post, _endpointUrl);

            request.Content = new StringContent(
                jsonPayload,
                Encoding.UTF8,
                "application/json");

            request.Headers.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer",
                    _apiKey.Trim());

            // Optional but recommended by OpenRouter
            request.Headers.Add(
                "X-Title",
                "Patient Grievance and Complaint Resolution");

            HttpResponseMessage response =
                await _httpClient.SendAsync(request);

            // Handle specific errors
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException(
                    "OpenRouter API key is invalid or expired.");
            }

            if (response.StatusCode == HttpStatusCode.ServiceUnavailable)
            {
                throw new HttpRequestException(
                    "OpenRouter service is temporarily unavailable.");
            }

            if (!response.IsSuccessStatusCode)
            {
                string errorBody =
                    await response.Content.ReadAsStringAsync();

                throw new HttpRequestException(
                    $"OpenRouter API Failed! Status: {(int)response.StatusCode}. Response: {errorBody}");
            }

            string responseBody =
                await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var openRouterResponse =
                JsonSerializer.Deserialize<OpenRouterResponseWrapper>(
                    responseBody,
                    options);

            string extractedJsonText =
                openRouterResponse?.Choices?[0]?.Message?.Content;

            if (string.IsNullOrWhiteSpace(extractedJsonText))
            {
                throw new Exception(
                    "Failed to extract content from OpenRouter response.");
            }

            var result =
                JsonSerializer.Deserialize<RoutingResult>(
                    extractedJsonText,
                    options);

            if (result == null)
            {
                throw new Exception(
                    "Failed to deserialize RoutingResult.");
            }

            return result;
        }

        #region OpenRouter Response Wrapper

        private class OpenRouterResponseWrapper
        {
            public Choice[] Choices { get; set; }
        }

        private class Choice
        {
            public Message Message { get; set; }
        }

        private class Message
        {
            public string Content { get; set; }
        }

        #endregion
    }
}