<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="interface_info.aspx.cs" Inherits="WebWeChat.Net.Admin.backstage.page.xtwh.interface_info" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <meta name="renderer" content="webkit">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <meta name="Author" content="larry" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="format-detection" content="telephone=no">

    <!--#include file="/Admin/backstage/plugins.aspx"-->
</head>
<body>
    <form class="layui-form" id="form1">

        <div class="layui-col-xs12 layui-col-sm12 layui-col-md12 layui-col-lg12 animated fadeInRightBig" style="padding: 20px">
            <section class="larry-panel">
                <div class="portlet-body">
                    <div class="layui-form-item">
                        <label class="layui-form-label">接口名称:</label>
                        <div class="layui-input-block">
                            <input type="text" lay-verify="required" autocomplete="off" placeholder="请输入接口名称" class="layui-input" runat="server" id="i_name" name="i_name">
                        </div>
                    </div>

                    <div class="layui-form-item">
                        <label class="layui-form-label">接口地址:</label>
                        <div class="layui-input-block">
                            <input type="text" lay-verify="required" placeholder="请输入接口地址" class="layui-input" runat="server" id="i_url" name="i_url">
                        </div>
                    </div>

                    <div class="layui-form-item">
                        <div class="layui-input-block">
                            <input type="button" class="layui-btn" lay-submit="" lay-filter="demo1" value="保存" id ="subBtn"/>
                            <button type="reset" class="layui-btn layui-btn-primary">重置</button>
                        </div>
                    </div>
                </div>
            </section>
        </div>
    </form>
    <!-- 注意：如果你直接复制所有代码到本地，上述js路径需要改成你本地的 -->
    <script>
        layui.use(['form', 'layedit', 'laydate'], function () {
            var form = layui.form
                , layer = layui.layer
                , layedit = layui.layedit
                , laydate = layui.laydate;

        });
    </script>

    <script>
        $(function () {

            $("#subBtn").click(function () {
                var i_name = $("#i_name").val();
                var i_url = $("#i_url").val();

                if ("" == i_name) {
                    layer.msg('接口名称不能为空', { icon: 7, shade: 0.6, time: 1000 });
                    return false;
                }
                if ("" == i_url) {
                    layer.msg('接口地址不能为空', { icon: 7, shade: 0.6, time: 1000 });
                    return false;
                }

                var params = $("#form1").serialize();
                console.log(params);
                $.ajax({
                    type: "POST",
                    url: "/SysManage/SaveorUpdateIterInfo",
                    data: params + "&id=" +<%=id%>,
                            success: function (result) {
                                if (result.rel == true) {
                                    layer.msg('保存成功', { icon: 1, shade: 0.6, time: 1000 });
                                    setTimeout("window.parent.layer.closeAll()", 1000);
                                }
                                else {
                                    layer.msg('保存失败', { icon: 7, shade: 0.6, time: 1000 });
                                }
                            }
                        });
            })
        })

    </script>
</body>
</html>
