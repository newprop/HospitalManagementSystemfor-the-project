﻿using FastReport.Web.Infrastructure;
using FastReport.Web.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace FastReport.Web.Controllers
{
    static partial class Controllers
    {
        internal sealed class ExportReportParams
        {
            public string ReportId { get; set; }

            public string ExportFormat { get; set; }
        }

        //[Authorize]
        [HttpGet("/preview.exportReport")]
        public static IResult ExportReport([FromQuery] ExportReportParams query,
            IReportService reportService,
            IExportsService exportsService,
            HttpRequest request)
        {
            if (!IsAuthorized(request))
                return Results.Unauthorized();

            if (!reportService.TryFindWebReport(query.ReportId, out WebReport webReport))
                return Results.NotFound();

            // TODO:
            // skip extra key/value pairs
            var exportFormat = query.ExportFormat.ToLower();
            var exportParams = request.Query.Where(pair => pair.Key != "exportFormat" && pair.Key != "reportId")
                .Select(item => new KeyValuePair<string, string>(item.Key, item.Value)).ToArray();
            byte[] file;
            string filename;

            try
            {
                file = exportsService.ExportReport(webReport, exportParams, exportFormat, out filename);
            }
            catch (Exception)
            {
                return Results.StatusCode((int)HttpStatusCode.UnsupportedMediaType);
            }

            return Results.File(file,
                contentType: MediaTypeNames.Application.Octet,
                fileDownloadName: $"{filename}.{exportFormat}");
        }

#if !OPENSOURCE
        [HttpPost("/preview.sendEmail")]
        public static async Task<IResult> SendEmail([FromQuery] string reportId,
            IReportService reportService,
            IExportsService exportsService,
            HttpRequest request)
        {
            if (!reportService.TryFindWebReport(reportId, out var webReport))
                return Results.NotFound();

            try
            {
                var form = await request.ReadFormAsync();

                var emailExportParams = new EmailExportParams
                {
                    Address = form["Address"],
                    Subject = form["Subject"],
                    MessageBody = form["MessageBody"],
                    ExportFormat = form["ExportFormat"],
                    NameAttachmentFile = form["NameAttachmentFile"]
                };

                exportsService.ExportEmail(webReport, emailExportParams);
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }

            return Results.Ok();
        }

#endif

        internal sealed class ExportSettingsParams
        {
            public string ReportId { get; set; }

            public string Format { get; set; }
        }

        [HttpPost("/exportsettings.getSettings")]
        public static IResult GetExportSettings([FromQuery] ExportSettingsParams query,
            IReportService reportService,
            IExportsService exportsService)
        {
            if (!reportService.TryFindWebReport(query.ReportId, out WebReport webReport))
                return Results.NotFound();

            var msg = exportsService.GetExportSettings(webReport, query.Format);

            if (msg != null)
            {
                return Results.Content(msg, MediaTypeNames.Text.Html);
            }
            return Results.NotFound();
        }
    }
}
