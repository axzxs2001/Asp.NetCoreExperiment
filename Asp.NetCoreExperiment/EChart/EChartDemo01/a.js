
//{
//    "divs": [
//        {
//            "id": "div1",
//            "class": "col"
//        },
//        {
//            "id": "div2",
//            "class": "col"
//        }
//    ],
//        "data": [
//            {
//                title: {
//                    text: 'ECharts 入门示例'
//                },
//                tooltip: {},
//                legend: {
//                    data: ['销量']
//                },
//                xAxis: {
//                    data: ['衬衫', '羊毛衫', '雪纺衫', '裤子', '高跟鞋', '袜子']
//                },
//                yAxis: {},
//                series: [
//                    {
//                        name: '销量',
//                        type: 'bar',
//                        data: [5, 20, 36, 10, 10, 20]
//                    }
//                ]
//            },
//            {

//                title: {
//                    text: 'ECharts 入门示例'
//                },
//                tooltip: {},
//                legend: {
//                    data: ['销量']
//                },
//                xAxis: {
//                    data: ['衬衫', '羊毛衫', '雪纺衫', '裤子', '高跟鞋', '袜子']
//                },
//                yAxis: {},
//                series: [
//                    {
//                        name: '销量',
//                        type: 'bar',
//                        data: [5, 20, 36, 10, 10, 20]
//                    }
//                ]

//            },
//        ]
//    ]

//}



var dic = document.getElementById('main')
$(dic).attr("class", "row");
$(dic).css("width", 1200);
$(dic).css("height", 800);

$(dic).html("<div class='col' id='a' style='width:600px;height:400px'></div><div class='col' id='b'  style='width:600px;height:400px'></div>")



var a = document.getElementById('a')
// 基于准备好的dom，初始化echarts实例
var myChart1 = echarts.init(a);

// 指定图表的配置项和数据
var option1 = {
    title: {
        text: 'ECharts 入门示例'
    },
    tooltip: {},
    legend: {
        data: ['销量']
    },
    xAxis: {
        data: ['衬衫', '羊毛衫', '雪纺衫', '裤子', '高跟鞋', '袜子']
    },
    yAxis: {},
    series: [
        {
            name: '销量',
            type: 'bar',
            data: [5, 20, 36, 10, 10, 20]
        }
    ]
};

// 使用刚指定的配置项和数据显示图表。
myChart1.setOption(option1);


var b = document.getElementById('b')
// 基于准备好的dom，初始化echarts实例
var myChart2 = echarts.init(b);

// 指定图表的配置项和数据
var option2 = {
    title: {
        text: 'ECharts 入门示例'
    },
    tooltip: {},
    legend: {
        data: ['销量']
    },
    xAxis: {
        data: ['衬衫', '羊毛衫', '雪纺衫', '裤子', '高跟鞋', '袜子']
    },
    yAxis: {},
    series: [
        {
            name: '销量',
            type: 'bar',
            data: [5, 20, 36, 10, 10, 20]
        }
    ]
};

// 使用刚指定的配置项和数据显示图表。
myChart2.setOption(option2);