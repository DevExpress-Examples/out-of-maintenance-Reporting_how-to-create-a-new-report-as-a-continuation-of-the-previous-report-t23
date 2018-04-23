using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Native;
using DevExpress.XtraReports.UI;

namespace dxSample {
    public partial class MainForm : DevExpress.XtraEditors.XtraForm {
        public MainForm() {
            InitializeComponent();

            XtraReport rep = new XtraReportMain();
            rep.CreateDocument();
            documentViewer1.DocumentSource = rep;
        }
        public float GetTopIndent() {
            float sumHeight = 0;
            XtraReportMain mainReport = documentViewer1.DocumentSource as XtraReportMain;
            NestedBrickIterator iterator = new NestedBrickIterator(mainReport.Pages[mainReport.Pages.Count - 1].InnerBricks);
            while (iterator.MoveNext()) {
                if (iterator.CurrentBrick is VisualBrick) {
                    VisualBrick brick = (VisualBrick)iterator.CurrentBrick;
                    float bottomPos = brick.Rect.Bottom;

                    if (bottomPos > sumHeight) {
                        sumHeight = bottomPos;
                    }
                }
            }
            sumHeight = GraphicsUnitConverter.Convert(sumHeight, GraphicsUnit.Document, GraphicsUnit.Inch) * 100;
            return sumHeight;
        }
        private void GenerateSecondReport() {
            XtraReport report = CreateReport();
            new ReportPrintTool(report).ShowPreview();
        }
        private XtraReport CreateReport() {
            float topIndent = GetTopIndent();
            XtraReport report = new XtraReport1();
            ReportHeaderBand reportHeaderBand = InitReportHeaderBand(topIndent, report);
            report.Bands.Add(reportHeaderBand);
            report.CreateDocument();
            return report;
        }
        private static ReportHeaderBand InitReportHeaderBand(float topIndent, XtraReport report) {
            ReportHeaderBand reportHeaderBand = report.Bands.OfType<ReportHeaderBand>().FirstOrDefault();
            XRPanel indentPanel = new XRPanel() {
                HeightF = topIndent, LocationF = new PointF(0, 0)
            };
            if(reportHeaderBand == null) {
                reportHeaderBand = new ReportHeaderBand();
                reportHeaderBand.Controls.Add(indentPanel);
            } else {
                reportHeaderBand.Controls.Add(indentPanel);
                reportHeaderBand.Controls.SetChildIndex(indentPanel, 0);
                reportHeaderBand.Controls[1].LocationF = new PointF(indentPanel.LocationF.X, indentPanel.BottomF);
            }
            return reportHeaderBand;
        }
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            GenerateSecondReport();
        }
    }
}
