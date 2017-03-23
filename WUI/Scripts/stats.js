//    var colors = ['#2b908f', '#90ee7e', '#f45b5b', '#7798BF', '#aaeeee', '#ff0066', '#eeaaee', '#55BF3B', '#DF5353', '#7798BF', '#aaeeee']

//Highcharts.theme = {
//    colors: colors,
//    chart: {
//        backgroundColor: {
//            linearGradient: { x1: 0, y1: 0, x2: 1, y2: 1 },
//            stops: [
//               [0, '#2a2a2b'],
//               [1, '#3e3e40']
//            ]
//        },
//        style: {
//            fontFamily: 'Roboto',
//        },
//        plotBorderColor: '#606063'
//    },

//    title: {
//        style: {
//            color: '#E0E0E3',
//            textTransform: 'uppercase',
//            fontSize: '20px'
//        }
//    },
//    subtitle: {
//        style: {
//            color: '#E0E0E3',
//            textTransform: 'uppercase'
//        }
//    },
//    tooltip: {
//        backgroundColor: 'rgba(0, 0, 0, 0.85)',
//        style: {
//            color: '#F0F0F0'
//        }
//    },
//    legend: {
//        itemStyle: {
//            color: '#E0E0E3'
//        },
//        itemHoverStyle: {
//            color: '#FFF'
//        },
//        itemHiddenStyle: {
//            color: '#606063'
//        }
//    },
//    plotOptions: {
//        colorByPoint: true,
//        series: {
//            dataLabels: {
//                color: colors
//            },
//            marker: {
//                lineColor: '#333'
//            }
//        },
//        boxplot: {
//            fillColor: '#505053'
//        },
//        candlestick: {
//            lineColor: 'white'
//        },
//        errorbar: {
//            color: 'white'
//        }
//    },
//    labels: {
//        style: {
//            color: '#fff'
//        }
//    },


//    scrollbar: {
//        barBackgroundColor: '#808083',
//        barBorderColor: '#808083',
//        buttonArrowColor: '#CCC',
//        buttonBackgroundColor: '#606063',
//        buttonBorderColor: '#606063',
//        rifleColor: '#FFF',
//        trackBackgroundColor: '#404043',
//        trackBorderColor: '#404043'
//    },

//    legendBackgroundColor: 'rgba(0, 0, 0, 0.5)',
//    background2: '#505053',
//    dataLabelsColor: '#B0B0B3',
//    textColor: '#C0C0C0',
//    contrastTextColor: '#F0F0F3',
//    maskColor: 'rgba(255,255,255,0.3)'
//};

//// Apply the theme
//Highcharts.setOptions(Highcharts.theme);


//Stats 1

Highcharts.chart('container', {
    chart: {
        type: 'column'
    },
    title: {
        text: 'Nombre participants par marathon'
    },
    subtitle: {
        text: 'Saison 2017'
    },

    xAxis: {
        categories: villes,
        color: "#fff",
        crosshair: true
    },
    yAxis: {
        min: 0,
        title: {
            text: ''
        }
    },
    tooltip: {
        headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
        pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
            '<td style="padding:0"><b>{point.y:.1f}</b></td></tr>',
        footerFormat: '</table>',
        shared: true,
        useHTML: true
    },
    plotOptions: {
        series: {
            dataLabels: {
                color: '#B0B0B3'
            },
            marker: {
                lineColor: '#333'
            }
        },
        boxplot: {
            fillColor: '#505053'
        },
        candlestick: {
            lineColor: 'white'
        },
        errorbar: {
            color: 'white'
        }
    },
    series: [{
        name: 'Total Participants',
        data: nbParticipants

    }],
});

//Stat2

Highcharts.chart('container2', {
    chart: {
        plotBackgroundColor: null,
        plotBorderWidth: null,
        plotShadow: false,
        type: 'pie'
    },
    title: {
        text: "Tranche d'âge des participants"
    },
    tooltip: {
        pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
    },
    plotOptions: {
        pie: {
            allowPointSelect: true,
            cursor: 'pointer',
            dataLabels: {
                enabled: true,
                format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                style: {
                    color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                }
            }
        }
    },
    series: [{
        name: "Tranche d'âge",
        colorByPoint: true,
        data: [{
            name: '[18-29]',
            y: 12
        }, {
            name: '[30-39]',
            y: 25,
            sliced: true,
            selected: true
        }, {
            name: '[40-49]',
            y:  40
        }, {
            name: '[+50]',
            y: 13
        }]
    }]
});