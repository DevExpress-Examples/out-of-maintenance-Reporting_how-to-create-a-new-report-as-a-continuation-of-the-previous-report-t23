<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128599573/14.2.7%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T234649)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* **[MainForm.cs](./CS/MainForm.cs) (VB: [MainForm.vb](./VB/MainForm.vb))**
* [Program.cs](./CS/Program.cs) (VB: [Program.vb](./VB/Program.vb))
* [XtraReport1.cs](./CS/Report/XtraReport1.cs) (VB: [XtraReport1.vb](./VB/Report/XtraReport1.vb))
* [XtraReportMain.cs](./CS/Report/XtraReportMain.cs) (VB: [XtraReportMain.vb](./VB/Report/XtraReportMain.vb))
* [SecondReportPreviewForm.cs](./CS/SecondReportPreviewForm.cs) (VB: [SecondReportPreviewForm.vb](./VB/SecondReportPreviewForm.vb))
<!-- default file list end -->
# How to create a new report as a continuation of the previous report


I have a <strong>static</strong> report which content does not reach the end of the last report page producing an empty space. <br /><br />I want my next report to start from the very position the first report ends. This example demonstrates how to generate the second report starting with the end position of the first report's content. <br /><br />1. Iterate through the last page Bricks to find the bottommost one. Do so by using the NestedBrickIterator class. <br />2. Convert the resulting height value to measurement units used in your Report (use the <a href="https://documentation.devexpress.com/#xtrareports/CustomDocument5134">GraphicsUnitConverter</a> class). <br />3. Create the next report and set the top indent equal to the calculated summary height. <br />For example, we can get the report's <a href="https://documentation.devexpress.com/#XtraReports/clsDevExpressXtraReportsUIReportHeaderBandtopic">ReportHeaderBand</a> (or create a new if it does not exist yet) and insert an <a href="https://documentation.devexpress.com/#xtrareports/clsDevExpressXtraReportsUIXRPaneltopic">XRPanel</a> into the <a href="https://documentation.devexpress.com/#XtraReports/clsDevExpressXtraReportsUIReportHeaderBandtopic">ReportHeaderBand</a>. To add an indent, set the XRPanel.<a href="https://documentation.devexpress.com/#XtraReports/DevExpressXtraReportsUIXRControl_HeightFtopic">HeightF</a> property to the calculated summary height value. <br />
<p>As a result, the second report will be visually rendered from the end point of the first report (i.e., with the defined indentation).</p>

<br/>


