<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="interface_list.aspx.cs" Inherits="WebWeChat.Net.Admin.backstage.page.xtwh.interface_list" %>

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


    <style>
        html {
            overflow-x: hidden;
        }

        th, td {
            white-space: nowrap;
            text-align: center
        }
    </style>
    <script>

        $(function () {

            var settings = {
                "lengthChange": true,
                "bProcessing": false,
                "searching": false,
                "ordering": false,
                "info": true,
                "pageLength": 10,
                "autoWidth": false,
                "serverSide": true,
                "pagingType": "full_numbers",
                "colReorder": {
                    "realtime": true
                },//列拖动顺

                "ajax": {
                    "url": "/SysManage/GetInterfaceList",
                    "type": "POST",
                    "dataType": "json",

                    "data": function (data) {
                        data["kw"] = $("#kw").val();
                    }
                },

                "aaSorting": [[2, "desc"]],
                "language": {
                    "sProcessing": "<div class=\"spinner\"><div class=\"bounce1\"></div><div class=\"bounce2\"></div><div class=\"bounce3\"></div></div>",
                    "sLengthMenu": "显示 _MENU_ 项结果",
                    "sZeroRecords": "没有匹配结果",
                    "sInfo": "显示第 _START_ 至 _END_ 项结果，共 _TOTAL_ 项",
                    "sInfoEmpty": "显示第 0 至 0 项结果，共 0 项",
                    "sInfoFiltered": "(由 _MAX_ 项结果过滤)",
                    "sInfoPostFix": "",
                    "sSearch": "搜索:",
                    "sUrl": "",
                    "sEmptyTable": "表中数据为空",
                    "sLoadingRecords": "载入中...",
                    "sInfoThousands": ",",
                    "oPaginate": {
                        "sFirst": "首页",
                        "sPrevious": "上页",
                        "sNext": "下页",
                        "sLast": "末页"
                    },
                    "oAria": {
                        "sSortAscending": ": 以升序排列此列",
                        "sSortDescending": ": 以降序排列此列"
                    }
                },

                "columns": [
                    null,
                    { "data": "interface_name" },
                    { "data": "interface_url" },
                    { "data": "stime" },
                    null,
                    null

                ],
                "columnDefs": [
                    {
                        "width": "5%",
                        "searchable": false,
                        "orderable": false,
                        "data": null,
                        "targets": [0],
                        "render": function (data, type, row, meta) {
                            return meta.row + _dataTables.page.info().start + 1;
                        }
                    },

                    {
                        "width": "150px",
                        "searchable": false,
                        "orderable": false,
                        "targets": [4],
                        "data": "isused",
                        "render": function (data, type, row, meta) {
                            var htmlContent = "";
                            if (data == true) {
                                htmlContent = "<button type=\"button\" class=\"layui-btn layui-btn-danger layui-btn-radius layui-btn-xs changeStatus\" id=\"" + row.id + "\">使用中</button>";
                            } else {
                                htmlContent = "<button type=\"button\" class=\"layui-btn  layui-btn-radius layui-btn-xs changeStatus\" id=\"" + row.id + "\">未使用</button>";
                            }
                            return htmlContent;
                        },
                    },

                    {
                        "width": "150px",
                        "searchable": false,
                        "orderable": false,
                        "targets": [5],
                        "data": "id",
                        "render": function (data, type, row, meta) {
                            var htmlContent = "<button  type=\"button\" class=\"layui-btn  layui-btn-xs add_edit_btn\" id=\"" + data + "\">编辑</button>";
                            htmlContent = htmlContent + "<button  type=\"button\" class=\"layui-btn layui-btn-danger layui-btn-xs delBtn\" style=\"margin-left:10px\" id=\"" + data + "\">删除</button>";
                            return htmlContent;
                        },
                    }
                ],


                "order": [
                    [0, 'asc']
                ],


                // set the initial value

                //"dom": "<'row' <'col-md-12'B>><'row'<'col-md-6 col-sm-12'l><'col-md-6 col-sm-12'f>r><'table-scrollable't><'row'<'col-md-5 col-sm-12'i><'col-md-7 col-sm-12'p>>", // horizobtal scrollable datatable
                "dom": "<'row'<'col-md-5'l><'col-md-7'<'#mydiv' and >>>r<'table-scrollable't><'row'<'col-sm-5'i><'col-sm-7'p>>"
            };
            var _dataTables = $('#dataTable').DataTable((settings));

            $('.date-picker').datepicker({
                language: 'zh-CN',
                autoclose: true,
                todayHighlight: true,
                format: 'yyyy-mm-dd',
            });

            $("body").delegate("#mybutton", "click", function () {
                _dataTables.ajax.reload();

            });


            $("body").delegate(".changeStatus", "click", function () {
                var id = this.id;
                layer.confirm('确认变更接口状态？', {
                    btn: ['确认', '取消'] //按钮
                }, function () {
                    $.ajax({
                        async: true,
                        type: "POST",
                        url: "/SysManage/ChangeIStatus",
                        data: { "id": id },
                        success: function (result) {
                            if (result.rel == true) {
                                layer.msg('状态变更成功！', { icon: 1, shade: 0.6, time: 1000 });
                                _dataTables.ajax.reload();

                            } else {
                                layer.msg('状态变更失败！', { icon: 7, shade: 0.6, time: 1000 });
                            }
                        },
                        error: function () {
                            layer.msg('状态变更失败！', { icon: 7, shade: 0.6, time: 1000 });
                        }
                    });
                });

            });

            $("body").delegate(".delBtn", "click", function () {
                var id = this.id;
                layer.confirm('正在使用的接口删除后可能会出现不可预知的错误，删除前请确保该接口未在使用。确认是否删除？', {
                    btn: ['确认', '取消'] //按钮
                }, function () {
                    $.ajax({
                        async: true,
                        type: "POST",
                        url: "/SysManage/DeleteInterfaceById",
                        data: { "id": id },
                        success: function (result) {
                            if (result.rel == true) {
                                layer.msg('删除成功！', { icon: 1, shade: 0.6, time: 1000 });
                                _dataTables.ajax.reload();

                            } else {
                                layer.msg('删除失败！', { icon: 7, shade: 0.6, time: 1000 });
                            }
                        },
                        error: function () {
                            layer.msg('删除失败！', { icon: 7, shade: 0.6, time: 1000 });
                        }
                    });
                });

            });

            $("body").delegate(".add_edit_btn", "click", function () {
                var flag = true;
                layer.open({
                    type: 2,
                    title: '接口信息',
                    maxmin: false,
                    shade: [0.5, '#393D49'],
                    resize: false,
                    shadeClose: false, //点击遮罩关闭层
                    area: ['500px', '260px'],
                    content: 'interface_info.aspx?id=' + this.id,
                    scrolling: 'no',
                    cancel: function (layero, index) {
                        flag = false;
                        layer.closeAll();
                        return false;
                    },
                    end: function () {
                        if (flag) {
                            //layer.msg('修改成功', { icon: 1, shade: 0.6, time: 1000 });
                            _dataTables.ajax.reload();
                        }
                    }
                });
            });

        })
    </script>
</head>
<body>
    <form id="form1" runat="server">

        <div class="layui-col-xs12 layui-col-sm12 layui-col-md12 layui-col-lg12 animated fadeInRightBig" style="padding: 20px">
            <section class="larry-panel">
                <div class="portlet-body" style="padding: 20px">

                    <div class="row" style="float: right">
                        <div style="float: right; margin-right: 10px">
                            <button type="button" class="layui-btn layui-btn-sm add_edit_btn" runat="server">新增</button>
                        </div>
                        <div style="float: right; margin-right: 20px">
                            <button type="button" id="mybutton" class="layui-btn layui-btn-sm">查询</button>
                        </div>
                        <div style="float: right; margin-right: 5px">
                            <input id="kw" name="kw" style="height: 30px; width: 200px; color: gray" placeholder="接口名称/地址" class="layui-input" />
                        </div>
                    </div>
                    <table id="dataTable" class="table table-striped table-bordered table-hover table-condensed" align="center">
                        <thead>
                            <tr>
                                <td>序号</td>
                                <td>接口名称</td>
                                <td>接口地址</td>
                                <td>修改时间</td>
                                <td>状态</td>
                                <td>操作</td>
                            </tr>
                        </thead>
                    </table>
                </div>
            </section>
        </div>

    </form>


</body>
</html>
