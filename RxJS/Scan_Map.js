//https://playcode.io/rxjs

import { of, scan, map } from 'rxjs';
 
const numbers$ = of(2, 4, 6);
 
numbers$
  .pipe(
    // Get the sum of the numbers coming in.
    scan((total, n) => { console.log(`${total}-${n}-${total + n}`); return total + n})
    // Get the average by dividing the sum by the total number
    // received so far (which is 1 more than the zero-based index).
    ,map((sum, index) =>{ console.log(`${sum}-${index+1}`); return sum / (index + 1)})
  )
  .subscribe(console.log);

/////////////////////////////////////////////////////////////////////
import { interval, scan, map, startWith } from 'rxjs';
 
const firstTwoFibs = [0, 1];
// An endless stream of Fibonacci numbers.
const fibonacci$ = interval(1000).pipe(
  // Scan to get the fibonacci numbers (after 0, 1)
  scan(([a, b]) => [b, a + b], firstTwoFibs),
  // Get the second number in the tuple, it's the one you calculated
  //map(([, n]) => n),
  // Start with our first two digits :)
  startWith(...firstTwoFibs)
);
 
 fibonacci$.subscribe(console.log);


/////////////////////////////////////////////////////////////////////
import { of, concatMap, interval, take, map, tap } from 'rxjs';

of(1, 2, 3).pipe(
  concatMap(n => interval(1000).pipe(
    take(Math.round(Math.random() * 10)),
    map(() => 'X'),
    tap({ complete: () => console.log(`Done with ${ n }`) })
  ))
)
.subscribe(console.log);
