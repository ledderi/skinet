export interface IQueryResult<T> {
    pageIndex: number;
    pageSize: number;
    count: number;
    data: T[];
}
