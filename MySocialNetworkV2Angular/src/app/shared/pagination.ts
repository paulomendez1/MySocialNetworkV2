export interface Pagination{
    totalCount : number;
    pageSize : number;
    currentPage : number;
    totalPages : number;
    previousPageLink : string;
    nextPageLink: string
}