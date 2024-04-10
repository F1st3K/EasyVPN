import { AxiosError } from "axios"

export default interface ApiError extends AxiosError<ErrorResponse> {}

interface ErrorResponse {
    type: string
    title: string
    status: number
    traceId: string
    errorCodes: string[] | null
}