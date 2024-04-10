export default interface ApiError {
    type: string
    title: string
    status: number
    traceId: string
    errorCodes: string[] | null
}