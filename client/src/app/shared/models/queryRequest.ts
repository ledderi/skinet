export interface IQueryRequest {
    pageIndex: number;
    pageSize: number;
    orderBy: string;
    orderDirection: string;
    productTypeId?: number;
    productBrandId?: number;
    search?: string;
}