import { useEffect, useState } from 'react';

export default function useIntervalCounter(minutes: number): number {
    const [count, setCount] = useState<number>(0);

    useEffect(() => {
        const interval = setInterval(
            () => {
                setCount((prevCount) => prevCount + 1);
            },
            minutes * 60 * 1000,
        );

        return () => clearInterval(interval);
    }, []);

    return count;
}
