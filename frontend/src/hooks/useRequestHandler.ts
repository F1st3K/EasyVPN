import { useState } from 'react';
import Page from '../api/requests/Page';

export default function useRequestHandler<TResponse = object, TError = Error>(
    request: () => Promise<TResponse>,
): [
    handler: (then?: (response: TResponse) => void) => void,
    loading: boolean,
    error: TError | null,
] {
    const [loading, setLoading] = useState<boolean>(false);
    const [error, setError] = useState<TError | null>(null);

    const handler = (then?: (response: TResponse) => void) => {
        setLoading(true);
        request()
            .then((v) => then && then(v))
            .catch((e) => {
                setError(e);
            })
            .finally(() => {
                setLoading(false);
            });
    };

    return [handler, loading, error];
}
