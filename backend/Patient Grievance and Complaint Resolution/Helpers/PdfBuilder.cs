using Patient_Grievance_and_Complaint_Resolution.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Patient_Grievance_and_Complaint_Resolution.Helpers
{
    public static class PdfBuilder
    {
        // ✅ MUST return byte[]
        public static byte[] BuildGrievanceReport(Grievance g, Resolution r)
        {
            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Content().Column(col =>
                    {
                        col.Item().Text("GRIEVANCE RESOLUTION REPORT")
                            .Bold()
                            .FontSize(18)
                            .AlignCenter();

                        col.Item().PaddingVertical(10);

                        col.Item().Text($"Patient MRN: {g.Patient.Mrn}");
                        //col.Item().Text($"Category: {g.Category.CategoryName}");
                        col.Item().Text($"Department: {g.Department.DepartmentName}");
                        col.Item().Text($"Grievance Date: {g.CreatedAt:dd-MMM-yyyy}");

                        col.Item().PaddingVertical(10);

                        col.Item().Text("ORIGINAL GRIEVANCE").Bold();
                        col.Item().Text(g.Description);

                        col.Item().PaddingVertical(10);

                        col.Item().Text("ROOT CAUSE").Bold();
                        col.Item().Text(r.RootCause);

                        col.Item().PaddingVertical(10);

                        col.Item().Text("CORRECTIVE ACTION").Bold();
                        col.Item().Text(r.CorrectiveAction);

                        col.Item().PaddingVertical(10);

                        col.Item().Text("PREVENTIVE ACTION").Bold();
                        col.Item().Text(r.PreventiveAction);

                        col.Item().PaddingVertical(10);

                        col.Item().Text("RESOLUTION SUMMARY").Bold();
                        col.Item().Text(r.ResolutionSummary);

                        col.Item().PaddingVertical(10);

                        col.Item().Text($"Resolved On: {r.ResolvedAt:dd-MMM-yyyy}");
                        col.Item().Text($"Investigated By: {r.Investigator.FullName}");
                    });
                });
            })
            .GeneratePdf();   // ✅ THIS LINE IS IMPORTANT
        }
    }
}