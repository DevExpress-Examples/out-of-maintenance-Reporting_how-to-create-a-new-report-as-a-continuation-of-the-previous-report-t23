Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Linq
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraPrinting.Native
Imports DevExpress.XtraReports.UI

Namespace dxSample
    Partial Public Class MainForm
        Inherits DevExpress.XtraEditors.XtraForm

        Public Sub New()
            InitializeComponent()

            Dim rep As XtraReport = New XtraReportMain()
            rep.CreateDocument()
            documentViewer1.DocumentSource = rep
        End Sub
        Public Function GetTopIndent() As Single
            Dim sumHeight As Single = 0
            Dim mainReport As XtraReportMain = TryCast(documentViewer1.DocumentSource, XtraReportMain)
            Dim [iterator] As New NestedBrickIterator(mainReport.Pages(mainReport.Pages.Count - 1).InnerBricks)
            Do While [iterator].MoveNext()
                If TypeOf [iterator].CurrentBrick Is VisualBrick Then
                    Dim brick As VisualBrick = CType([iterator].CurrentBrick, VisualBrick)
                    Dim bottomPos As Single = brick.Rect.Bottom

                    If bottomPos > sumHeight Then
                        sumHeight = bottomPos
                    End If
                End If
            Loop
            sumHeight = GraphicsUnitConverter.Convert(sumHeight, GraphicsUnit.Document, GraphicsUnit.Inch) * 100
            Return sumHeight
        End Function
        Private Sub GenerateSecondReport()
            Dim report As XtraReport = CreateReport()
            CType(New ReportPrintTool(report), ReportPrintTool).ShowPreview()
        End Sub
        Private Function CreateReport() As XtraReport
            Dim topIndent As Single = GetTopIndent()
            Dim report As XtraReport = New XtraReport1()
            Dim reportHeaderBand As ReportHeaderBand = InitReportHeaderBand(topIndent, report)
            report.Bands.Add(reportHeaderBand)
            report.CreateDocument()
            Return report
        End Function
        Private Shared Function InitReportHeaderBand(ByVal topIndent As Single, ByVal report As XtraReport) As ReportHeaderBand
            Dim reportHeaderBand As ReportHeaderBand = report.Bands.OfType(Of ReportHeaderBand)().FirstOrDefault()
            Dim indentPanel As New XRPanel() With {.HeightF = topIndent, .LocationF = New PointF(0, 0)}
            If reportHeaderBand Is Nothing Then
                reportHeaderBand = New ReportHeaderBand()
                reportHeaderBand.Controls.Add(indentPanel)
            Else
                reportHeaderBand.Controls.Add(indentPanel)
                reportHeaderBand.Controls.SetChildIndex(indentPanel, 0)
                reportHeaderBand.Controls(1).LocationF = New PointF(indentPanel.LocationF.X, indentPanel.BottomF)
            End If
            Return reportHeaderBand
        End Function
        Private Sub barButtonItem3_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles barButtonItem3.ItemClick
            GenerateSecondReport()
        End Sub
    End Class
End Namespace
