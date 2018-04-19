<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="WebWeChat.Net.Admin.backstage.page.main" %>

<!DOCTYPE html>
<html>
<head>
	<meta charset="utf-8">
	<title>后台首页</title>
    <meta name="renderer" content="webkit">	
	<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">	
    <meta name="Author" content="larry" />
	<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
	<meta name="apple-mobile-web-app-status-bar-style" content="black">	
	<meta name="apple-mobile-web-app-capable" content="yes">	
	<meta name="format-detection" content="telephone=no">	
	<link rel="Shortcut Icon" href="/favicon.ico" />
	<link rel="stylesheet" type="text/css" href="../../common/layui/css/layui.css" media="all">
    <link rel="stylesheet" type="text/css" href="../css/base.css" media="all">
    <link rel="stylesheet" type="text/css" href="../../common/css/animate.css" media="all">
    <link rel="stylesheet" type="text/css" href="../css/main.css">

     <!-- ECharts单文件引入 -->
    <script src="/assets/echarts/echarts.js"></script>
    <script type="text/javascript">

        // 路径配置
        require.config({
            paths: {
                echarts: '/assets/echarts'
            }
        });

        // 使用
        require(
            [
                'echarts',
                'echarts/chart/pie' // 使用柱状图就加载bar模块，按需加载
            ],
            function (ec) {
                var ac = ""
                var data = new Array();
                $.post("/Tjfx/TztjfxWxB1", ac, function (ajaxObj, status) {

                    var jsonData = eval("(" + ajaxObj + ")");
                    $.each(jsonData, function (n, value) {
                        var obj = {};
                        obj['name'] = value.name;
                        data.push(obj);
                    });

                    var myChart = ec.init(document.getElementById('wxB'));
                    option = {
                        title: {
                            text: '设备总体维修情况统计二',
                            subtext: '统计全部数据',
                            x: 'center'
                        },
                        tooltip: {
                            trigger: 'item',
                            formatter: "{a} <br/>{b} : {c} ({d}%)"
                        },
                        legend: {
                            orient: 'vertical',
                            x: 'left',
                            data: data
                        },
                        toolbox: {
                            show: true,
                            feature: {
                                restore: { show: true },
                                saveAsImage: { show: true }
                            }
                        },
                        series: [
                            {
                                name: '设备状态',
                                type: 'pie',
                                radius: '55%',
                                center: ['50%', '60%'],
                                data: jsonData
                            }
                        ]
                    };
                    // 为echarts对象加载数据 
                    myChart.setOption(option);
                });
            }
        );

    </script>
</head>
<body class="larry-bg-gray">
<div class="layui-fluid">
	<div class="larry-container animated fadeIn">
        <div class="layui-row layui-col-space15" id="Minfo">
        	<div class="layui-col-xs12 layui-col-sm12 layui-col-md12 layui-col-lg12">
        		 <div class="head-info larry-font-theme layui-bg-green">
        		 	<div class="center" style="font-size:16px"><%=Session["nickname"] %><span id="weather"></span></div>
        		 	<i class="larry-icon larry-guanbi1" id="closeInfo"></i>
        		 </div>
        	</div>
        </div>

        <!-- 快捷导航 -->
        <div class="layui-row larry-shortcut layui-col-space15" id="shortcut">
        	<div class="layui-col-xs12 layui-col-sm6 layui-col-md3 <%=lg_class %> larry-col" runat="server" id="tongji_a">
                <section class="shortcut larry-ico-rotate">
                    <a class="pos-r" data-url='html/main.html' data-icon="larry-neirongguanli1">
                        <div class="larry-ico larry-bg-blue"><i class="larry-icon larry-tongjishuju"></i></div>
                        <div class="larry-value">
                            <span class="nums"><asp:Label ID="labTotalNumber" runat="server" Text="1000"></asp:Label>人</span>
                            <cite>注册医师数</cite>
                        </div>
                    </a>
                </section>
            </div>
            <div class="layui-col-xs12 layui-col-sm6 layui-col-md3 <%=lg_class %> larry-col">
                <section class="shortcut pos-r larry-ico-rotate">
                    <a class="pos-r" data-url='html/main.html' data-icon="larry-huiyuanguanli">
                        <div class="larry-ico larry-bg-red"><i class="larry-icon larry-tongjishuju"></i></div>
                        <div class="larry-value">
                            <span class="nums"><asp:Label ID="labZxNumber" runat="server" Text="1000"></asp:Label>人</span>
                            <cite>咨询患者数</cite>
                        </div>
                    </a>
                </section>
            </div>
            <div class="layui-col-xs12 layui-col-sm6 layui-col-md3 <%=lg_class %> larry-col">
                <section class="shortcut pos-r larry-ico-rotate">
                    <a class="pos-r" data-url='html/main.html' data-icon="larry-huiyuanguanli">
                        <div class="larry-ico larry-bg-green"><i class="larry-icon larry-tongjishuju"></i></div>
                        <div class="larry-value">
                            <span class="nums"><asp:Label ID="labSjNumber" runat="server" Text="1000"></asp:Label>人</span>
                            <cite>收集会话数）</cite>
                        </div>
                    </a>
                </section>
            </div>
            <div class="layui-col-xs12 layui-col-sm6 layui-col-md3 <%=lg_class %> larry-col">
                <section class="shortcut pos-r larry-ico-rotate">
                    <a class="pos-r" data-url='html/main.html' data-icon="larry-neirongfabu">
                        <div class="larry-ico larry-bg-purple"><i class="larry-icon larry-tongjishuju"></i></div>
                        <div class="larry-value">
                            <span class="nums"><asp:Label ID="labBfNumber" runat="server" Text="1000"></asp:Label>人</span>
                            <cite> 新增患者数（近一周）</cite>
                        </div>
                    </a>
                </section>
            </div>
          
        </div>

        <!-- 面板 -->
        <div class="layui-row layui-col-space15">
           
            <!-- 网站数据统计 -->
            <div class="layui-col-xs12 layui-col-sm12 layui-col-md12 layui-col-lg12">
                <section class="larry-panel">
                    <div class="larry-panel-header">
                        <h3 class="larry-panel-title">数据概览</h3>
                        <i class="larry-icon larry-unfold1 tools"></i>
                    </div>
                    <div class="larry-panel-body">
                        <div id="wxB" style="height: 300px"></div>
                    </div>
                </section>
            </div>

        </div>
   

	</div>
</div>
<!-- 加载js文件-->
<script type="text/javascript" src="../../common/layui/layui.js"></script>
<script type="text/javascript">
layui.cache.page = 'main'; 
layui.config({
   version:"2.0.7",
   base:'../../common/'
}).extend({
    larry:'js/base'
}).use('larry');
</script>
</body>
</html>
