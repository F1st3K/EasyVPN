import { useEffect, useState } from "react"


export default function useRequest<TResponse = object, TError = Error>(request: () => Promise<TResponse>) 
    : [data: TResponse | null, loading: boolean, error: TError | null] {
    const [data, setData] = useState<TResponse | null>(null)
    const [loading, setLoading] = useState<boolean>(false)
    const [error, setError] = useState<TError | null>(null)

    useEffect(() => {
        setLoading(true)
        request()
            .then(r => setData(r))
            .catch(e => setError(e))
            .finally(() => setLoading(false))
    }, [])

    return [data, loading, error]
}