<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DataViewTemplate.ascx.cs" Inherits="BPIWABK.Web.Images.DataViewTemplate" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<style type="text/css">
    .auto-style1 {
        height: 124px;
    }
</style>

<dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%">
    <PanelCollection>
        <dx:PanelContent runat="server">
            <table style="width: 100%">
                <tr>
                    <td class="auto-style1">
                        <dx:ASPxBinaryImage ID="ASPxBinaryImage1" runat="server" ContentBytes='<%# Eval("CoverImage") %>' EnableViewState="False" Width="200px" Height="120px" EnableServerResize="True" ImageSizeMode="FillAndCrop" ShowLoadingImage="True">
                        </dx:ASPxBinaryImage>
                    </td>
                    <td class="auto-style1">
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <dx:ASPxLabel ID="ASPxTitle" runat="server" Text='<%# Eval("Title") %>'>
                                    </dx:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>Post Date:
                                                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='<%# Eval("PostDate") %>'></dx:ASPxLabel>
                                            </td>
                                            <td>Posted By:
                                                <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text='<%# Eval("PostedBy") %>'></dx:ASPxLabel>
                                            </td>
                                            <td>Source:
                                                <dx:ASPxHyperLink ID="ASPxHyperLink1" runat="server" Text='<%# Eval("SourceName") %>' NavigateUrl='<%# Eval("SourceURL") %>'>
                                                </dx:ASPxHyperLink>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text='<%# Eval("Content") %>'></dx:ASPxLabel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxPanel>

