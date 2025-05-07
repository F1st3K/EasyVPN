import { useState } from 'react';

export default function useRequestHandler<
    TResponse = object,
    TError = Error,
    TParams = null,
>(
    request: (params: TParams) => Promise<TResponse>,
): [
    handler: (params: TParams, then?: (response: TResponse) => void) => void,
    loading: boolean,
    error: TError | null,
] {
    const [loading, setLoading] = useState<boolean>(false);
    const [error, setError] = useState<TError | null>(null);

    const handler = (params: TParams, then?: (response: TResponse) => void) => {
        setLoading(true);
        request(params)
            .then((v) => {
                setError(null);
                then && then(v);
            })
            .catch((e) => {
                setError(e);
            })
            .finally(() => {
                setLoading(false);
            });
    };

    return [handler, loading, error];
}
