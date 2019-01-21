import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ChartData } from './Data';
import { Chart } from 'chart.js';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'app';
  data: ChartData[];
  // url = 'http://localhost:3000/results';
  url = 'https://localhost:44380/Chart/GetChartDataByDay?currencyName=BTC&fromDay=0';
  month = [];
  cost = [];
  chart = [];
  constructor(private httpClient: HttpClient) {}

  ngOnInit() {
    // const httpH = new HttpHeaders();
    // httpH.append('Access-Control-Allow-Origin', '*');
    this.httpClient.get(this.url).subscribe((res: ChartData[]) => {
      res.forEach(y => {
        this.month.push(y.month);
        this.cost.push(y.cost);
      });
      this.chart = new Chart('canvas', {
        type: 'line',
        data: {
          labels: this.month,
          datasets: [
            {
              data: this.cost,
              borderColor: '#3cba9f',
              fill: false
            }
          ]
        },
        options: {
          legend: {
            display: false
          },
          scales: {
            xAxes: [{
              display: true
            }],
            yAxes: [{
              display: true
            }],
          }
        }
      });
    });
  }
}
