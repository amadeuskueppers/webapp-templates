import { SelectionModel } from '@angular/cdk/collections';
import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Mitarbeiter } from 'src/shared/models/mitarbeiter';
import { EmployeeService } from '../services/employee.service';

@Component({
  selector: 'app-employee-overview',
  templateUrl: './employee-overview.component.html',
  styleUrls: ['./employee-overview.component.scss']
})
export class EmployeeOverviewComponent implements OnInit {
  displayedColumns: string[] = ['select', 'nachname', 'vorname', 'geburtsdatum', 'isAzubi'];
  dataSource = new MatTableDataSource<Mitarbeiter>();
  selection = new SelectionModel<Mitarbeiter>(true, []);

  constructor(private employeeService: EmployeeService) {
    console.log('inside employee service..');
  }

  ngOnInit(): void {
    console.log('handling onInit of -employeeOverview-');
    this.loadData();
  }

  loadData() {
    this.selection = new SelectionModel<Mitarbeiter>(true, []);
    this.employeeService.getAll().subscribe((res) => {
      var entries = res as Mitarbeiter[];
      this.dataSource = new MatTableDataSource<Mitarbeiter>(entries);
    }, (err) => {
      console.log(err);
    })
  }

  /** Whether the number of selected elements matches the total number of rows. */
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSource.data.length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    this.isAllSelected() ?
      this.selection.clear() :
      this.dataSource.data.forEach((row: Mitarbeiter) => {
        return this.selection.select(row);
      });
  }

  /** The label for the checkbox on the passed row */
  checkboxLabel(row?: Mitarbeiter): string {
    if (!row) {
      return `${this.isAllSelected() ? 'select' : 'deselect'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.id + 1}`;
  }

  isAzubiDisplaytext(isAzubi: boolean): string {
    var text = isAzubi ? 'ja' : 'nein';
    return text;
  }

  deleteSelectedEntries() {
    this.selection.selected.forEach(entry => {
      console.log('Will delete selected entry', entry);
      this.employeeService.delete(entry.id).subscribe(() => {
        // nothing todo
      }, (err) => {
        console.error(`deletion of entry with id ${entry.id} failed!`, err);
      });
    });
  }

}
