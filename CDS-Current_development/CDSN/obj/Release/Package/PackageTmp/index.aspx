<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="CDSN.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Multi-Series Line Chart</title>
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <canvas id="myChart" width="400" height="200"></canvas>
        </div>
    </form>
    <script>
        const ctx = document.getElementById('myChart').getContext('2d');
        const data = {
            labels: series1Data.map(dp => dp.Label),
            datasets: [
                {
                    label: 'Series 1',
                    data: series1Data.map(dp => dp.Value),
                    borderColor: 'rgba(75, 192, 192, 1)',
                    backgroundColor: 'rgba(75, 192, 192, 0.2)',
                    fill: true,
                },
                {
                    label: 'Series 2',
                    data: series2Data.map(dp => dp.Value),
                    borderColor: 'rgba(255, 99, 132, 1)',
                    backgroundColor: 'rgba(255, 99, 132, 0.2)',
                    fill: true,
                }
            ]
        };

        const config = {
            type: 'line',
            data: data,
            options: {
                responsive: true,
                scales: {
                    y: {
                        beginAtZero: true
                    }
                }
            }
        };

        const myChart = new Chart(ctx, config);
    </script>
</body>
</html>