import { useState, useEffect } from "react";

// our hook
export default function useDebounce(value:any){
  // state and setters for debounce value
  const [debounceValue, setDebounceValue] = useState(value);

  useEffect(
    () => {
      // set debounceValue to value (passed in) after the specific delay
      const handler = setTimeout(() => {
        setDebounceValue(value);
      }, 500);

      return () => {
        clearTimeout(handler);
      };
    },
    [value]
    );

    return debounceValue;
  }
