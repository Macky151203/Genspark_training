import { Component, OnInit } from '@angular/core';
import { Dashboardservice } from '../services/dashboardservice';
import { CommonModule } from '@angular/common';
import { AgCharts } from 'ag-charts-angular';
import { topology } from '../Misc/topology';
import type { AgPieSeriesOptions, AgChartOptions } from 'ag-charts-community';

@Component({
  selector: 'app-dashboard',
  imports: [AgCharts, CommonModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css',
})
export class Dashboard implements OnInit {
  users: any[] = [];
  malecount: number = 0;
  femalecount: number = 0;
  departmentcounts: any = {};
  countrycounts: any = {};
  public pieoptions: any = {};
  public baroptions: any = {};
  public countryoptions: any = {};

  constructor(private dashboardservice: Dashboardservice) {}

  ngOnInit(): void {
    this.dashboardservice.getallusers().subscribe({
      next: (data) => {
        this.users = data.users;

        this.countGenders();
        this.departmentcounts = this.getdepartmentCount();
        this.countrycounts = this.getcountryCount();

        this.countryoptions = {
          data: Object.entries(this.countrycounts).map(
            ([state, count]) => ({
              state,
              count,
            })
          ),
          topology,
          series: [
            {
              type: 'map-shape-background',
            },
            {
              type: 'map-shape',
              title: 'Access to Clean Fuels',
              idKey: 'name',
              colorKey: 'value',
              colorName: '% of population',
            },
          ],
          gradientLegend: {
            enabled: true,
            position: 'right',
            gradient: {
              preferredLength: 200,
              thickness: 2,
            },
            scale: {
              label: {
                fontSize: 10,
                formatter: (p:any) => p.value + '%',
              },
            },
          },
        };

        this.baroptions = {
          data: Object.entries(this.departmentcounts).map(
            ([department, count]) => ({
              department,
              count,
            })
          ),
          title: {
            text: 'Department Count',
          },
          series: [
            {
              type: 'bar',
              xKey: 'department',
              yKey: 'count',
              labelKey: 'department',
            },
          ],
        };

        this.pieoptions = {
          data: [
            { gender: 'Male', count: this.malecount },
            { gender: 'Female', count: this.femalecount },
          ],
          title: {
            text: 'Gender Distribution',
          },
          series: [
            {
              type: 'pie',
              angleKey: 'count',
              legendItemKey: 'gender',
            },
          ],
        };
      },
      error: (err) => {
        console.error('Error fetching users:', err);
      },
    });
  }

  countGenders(): void {
    this.malecount = this.users.filter((user) => user.gender === 'male').length;
    this.femalecount = this.users.filter(
      (user) => user.gender === 'female'
    ).length;
  }

  getdepartmentCount(): any {
    const departmentCounts: { [key: string]: number } = {};
    this.users.forEach((user) => {
      if (user.company.department) {
        departmentCounts[user.company.department] =
          (departmentCounts[user.company.department] || 0) + 1;
      }
    });

    return departmentCounts;
  }

  getcountryCount(): any {
    const countryCounts: { [key: string]: number } = {};
    this.users.forEach((user) => {
      if (user.address.state) {
        countryCounts[user.address.state] =
          (countryCounts[user.address.state] || 0) + 1;
      }
    });
    console.log(countryCounts);

    return countryCounts;
  }
}
