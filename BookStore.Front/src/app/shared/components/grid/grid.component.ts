import { Component, OnInit, Input, ViewChild, Output, EventEmitter } from '@angular/core';
import { LazyLoadEvent } from 'primeng/api';
import { Table } from 'primeng/table';
import uuid from 'uuid';

@Component({
  selector: 'app-grid',
  templateUrl: './grid.component.html',
  styleUrls: ['./grid.component.css']
})
export class GridComponent implements OnInit {

  @Input() result: any[];
  @Input() rows: number;
  @Input() displayedColumns: any[];
  @Input() firstPage: number;
  @Input() rowsPerPage: number[] = [5, 10, 25, 50, 100, 150];
  @Input() totalRecords: number;
  @ViewChild('dt') table: Table;
  @Input() loading: boolean;
  @Input() hasEdit: boolean = false;
  @Input() hasDelete: boolean = false;
  @Output() gridLoad = new EventEmitter();
  @Output() delete = new EventEmitter();
  @Output() edit = new EventEmitter();

  constructor() {
    this.result = [];
    this.totalRecords = this.result.length;
    this.rows = 10;
    this.firstPage = 0;
  }

  ngOnInit(): void {
  }

  onGridLoad(event: LazyLoadEvent): void {
    this.gridLoad.emit(event);
  }

  onEdit(id: uuid): void {
    this.edit.emit(id);
  }

  onDelete(id: uuid): void {
    this.delete.emit(id);
  }
}
