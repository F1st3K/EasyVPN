import { useEffect, useState } from 'react';

export default function useRequest<TResponse = object, TError = Error>(
    request: () => Promise<TResponse>,
    deps: React.DependencyList = [],
): [data: TResponse | null, loading: boolean, error: TError | null] {
    const [data, setData] = useState<TResponse | null>(null);
    const [loading, setLoading] = useState<boolean>(true);
    const [error, setError] = useState<TError | null>(null);

    useEffect(() => {
        setLoading(true);
        setError(null);
        request()
            .then((r) => setData(r))
            .catch((e) => setError(e))
            .finally(() => setLoading(false));
    }, deps);

    return [data, loading, error];
}
