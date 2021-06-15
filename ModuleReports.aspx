<%@ Page Title="" Language="C#" MasterPageFile="~/Manager.Master" AutoEventWireup="true" CodeBehind="ModuleReports.aspx.cs" Inherits="QuadCore_Website.ModuleReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <!--<script type="text/javascript">
      google.charts.load("current", {packages:["corechart"]});
      google.charts.setOnLoadCallback(drawChart);

      function drawChart() {

        var arr = []; // array needed to be converted into graph
        arr[0] = []; // making index 0 and array
        arr[0][0] = 'Modules'; // name of the first col
        arr[0][1] = 'Students'; // name of the second col

          fetch('https://tuberfinal.azurewebsites.net/api/Report/GetNoOfStudentsPerModule')
            .then(res => res.json())
            .then(data => {

                    console.log(data.length);
                    console.log(data);

                    for(var i = 0; i < data.length; i++)
                    {
                        var index = i + 1;
                        arr[index] = [];
                        for(var j = 0; j < 2; j++)
                        {
                            if(j == 1) // check if its object name or data section
                            {
                                //console.log('Data ' + index + ' ' + j);
                                arr[index][j] = data[i].doubleValue;
                            }
                            else
                            {
                                //console.log('Description ' + index + ' ' + j);
                                arr[index][j] = data[i].moduleName;
                            }
                        }
                    }

                    var data = google.visualization.arrayToDataTable(arr);

                    var options = {
                    title: 'Number of students per modules',
                    is3D: true,
                    };

                    var chart = new google.visualization.PieChart(document.getElementById('piechart_3d'));
                    chart.draw(data, options);                       
                
                })
            .catch(err => console.log(err));
                
       
      }
    </script>-->

    <script type="text/javascript">
        google.charts.load('current', { packages: ['corechart', 'bar'] });
        google.charts.setOnLoadCallback(drawBasic);

        function drawBasic() {

            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Name of Module');
            data.addColumn('number', 'Students');
            data.addColumn({ type: 'string', role: 'Style' });

            fetch('https://tuberapi20201006100334.azurewebsites.net/api/Report/GetNoOfStudentsPerModule')
                .then(res => res.json())
                .then(objs => {

                    console.log(objs.length);
                    console.log(objs);

                    for (var i = 0; i < objs.length; i++) {
                        data.addRow([objs[i].moduleName, objs[i].doubleValue, 'color: orangered']);
                    }


                    var options = {
                        title: 'Number of students per module',
                        hAxis: {
                            title: 'Modules',
                            viewWindow: {
                                min: [20, 30, 0],
                                max: [50, 30, 0]
                            }
                        },
                        vAxis: {
                            title: 'Number of students'
                        }
                    };

                    var chart = new google.visualization.ColumnChart(
                        document.getElementById('chart_div'));

                    chart.draw(data, options);
                });

        }
    </script>

    <script type="text/javascript">
        google.charts.load("current", { packages: ["corechart"] });
        google.charts.setOnLoadCallback(drawChart);

        function drawChart() {

            var arr = []; // array needed to be converted into graph
            arr[0] = []; // making index 0 and array
            arr[0][0] = 'Module'; // name of the first col
            arr[0][1] = 'Income'; // name of the second col


            fetch('https://tuberapi20201006100334.azurewebsites.net/api/Report/getGrossIncomePerModule')
                .then(res => res.json())
                .then(data => {

                    console.log(data.length);
                    console.log(data);

                    for (var i = 0; i < data.length; i++) {
                        var index = i + 1;
                        arr[index] = [];
                        for (var j = 0; j < 2; j++) {
                            if (j == 1) // check if its object label or data section
                            {
                                // data section
                                arr[index][j] = data[i].doubleValue;
                            }
                            else
                            {
                                // label section
                                arr[index][j] = data[i].moduleName;
                            }
                        }
                    }

                    var data = google.visualization.arrayToDataTable(arr);

                    var options = {
                        title: 'Income Per Module',
                        pieHole: 0.4,
                    };

                    var chart = new google.visualization.PieChart(document.getElementById('donutchart'));
                    chart.draw(data, options);

                })
                .catch(err => console.log(err));

        }
    </script>


     <script type="text/javascript">
         google.charts.load('current', { packages: ['corechart', 'bar'] });
         google.charts.setOnLoadCallback(drawBasic);

         function drawBasic() {

             var data = new google.visualization.DataTable();
             data.addColumn('string', 'Name of Module');
             data.addColumn('number', 'No of Bookings');
             data.addColumn({ type: 'string', role: 'Style' });

             fetch('https://tuberapi20201006100334.azurewebsites.net/api/Report/getNoOfBookingsPerModule')
                 .then(res => res.json())
                 .then(objs => {

                     console.log(objs.length);
                     console.log(objs);

                     for (var i = 0; i < objs.length; i++) {
                         data.addRow([objs[i].moduleName, objs[i].doubleValue, 'color: orangered']);
                     }


                     var options = {
                         title: 'Number Of Outstanding Bookings Per Module',
                         hAxis: {
                             title: 'Modules',
                             viewWindow: {
                                 min: [20, 30, 0],
                                 max: [50, 30, 0]
                             }
                         },
                         vAxis: {
                             title: 'Number of Bookings'
                         }
                     };

                     var chart = new google.visualization.ColumnChart(
                         document.getElementById('interChart'));

                     chart.draw(data, options);
                 });

         }
    </script>
   
     
    <script type="text/javascript">
        google.charts.load('current', { packages: ['corechart', 'bar'] });
        google.charts.setOnLoadCallback(drawBasic);

        function drawBasic() {

            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Name of Module');
            data.addColumn('number', 'Tutors');
            data.addColumn({ type: 'string', role: 'Style' });

            fetch('https://tuberapi20201006100334.azurewebsites.net/api/Report/getNoOfTutorsPerModule')
                .then(res => res.json())
                .then(objs => {

                    console.log(objs.length);
                    console.log(objs);

                    for (var i = 0; i < objs.length; i++) {
                        data.addRow([objs[i].moduleName, objs[i].doubleValue, "color: orangered"]);
                    }


                    var options = {
                        title: 'Number of tutors per module',
                        hAxis: {
                            title: 'Modules',
                            viewWindow: {
                                min: [20, 30, 0],
                                max: [50, 30, 0]
                            }
                        },
                        vAxis: {
                            title: 'Number of tutors'
                        }
                    };

                    var chart = new google.visualization.ColumnChart(
                        document.getElementById('chart_tutors'));

                    chart.draw(data, options);
                });

        }
    </script>
   

    <h2 class="mb-4">Module Reports</h2>

     <table class="columns">
       <tr>
            <td id="chart_div" style="width: 900px; height: 500px;"></td>
            <td class ="col" id="donutchart" style="width: 900px; height: 500px;"></td>
       </tr>
       <tr> 
            <td id="interChart" style="width: 900px; height: 500px;"></td>
            <td id="chart_tutors" style="width: 900px; height: 500px;"></td>
            
       </tr>
    </table>
</asp:Content>