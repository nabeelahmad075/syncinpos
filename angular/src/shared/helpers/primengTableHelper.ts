import { Table } from "primeng/table/table";
import { LazyLoadEvent } from "primeng/api/public_api";
import { Paginator } from 'primeng/paginator/paginator';

export class PrimengTableHelper {
  predefinedRecordsCountPerPage = [25, 50, 100, { showAll: 'All' }];

  defaultRecordsCountPerPage = <number>this.predefinedRecordsCountPerPage[0];

  modalRowsPerPageOptions = [5];

  modalRowsCount = <number>this.modalRowsPerPageOptions[0];

  isResponsive = true;

  resizableColumns: false;

  totalRecordsCount = 0;

  records: any[];

  isLoading = false;

  static addRow(prevrowId: number, row: any, data: any[]) {
    const index = data.findIndex(a => a.id == prevrowId);
    if (index != -1) {
      data.splice(index + 1, 0, row);
    }
  }

  showLoadingIndicator(): void {
    setTimeout(() => {
      this.isLoading = true;
    }, 0);
  }

  hideLoadingIndicator(): void {
    setTimeout(() => {
      this.isLoading = false;
    }, 0);
  }

  getSorting(table: Table): string {
    let sorting;
    if (table.sortField) {
      sorting = table.sortField;
      if (table.sortOrder === 1) {
        sorting += ' ASC';
      } else if (table.sortOrder === -1) {
        sorting += ' DESC';
      }
    }

    return sorting;
  }

  getMaxResultCount(paginator: Paginator, event: LazyLoadEvent): number {
    if (paginator.rows) {
      return paginator.rows;
    }

    if (!event) {
      return this.defaultRecordsCountPerPage;
    }

    return event?.rows ?? this.defaultRecordsCountPerPage;
  }

  getModalMaxResultCount(paginator: Paginator, event: LazyLoadEvent): number {
    
    if (paginator.rows) {
      
      return paginator.rows;
    }

    if (!event) {
      return this.modalRowsCount;
    }

    return event?.rows ?? this.modalRowsCount;
  }

  getSkipCount(paginator: Paginator, event: LazyLoadEvent): number {
    if (paginator.first) {
      return paginator.first;
    }

    if (!event) {
      return 0;
    }

    return event.first;
  }

  shouldResetPaging(event: LazyLoadEvent): boolean {
    if (!event /*|| event.sortField*/) { // if you want to reset after sorting, comment out parameter
      return true;
    }

    return false;
  }
}
