import { of, scan, map } from 'rxjs';

const numbers$ = of(1,5, 7, 9);

numbers$
  .pipe(
    // Get the sum of the numbers coming in.
    scan((total, n) => 
    {
      console.log(`scan: acc:${total} - value:${n}`);
      var res=(total + n);
     return res;
    })
    // Get the average by dividing the sum by the total number
    // received so far (which is 1 more than the zero-based index).
    ,map((sum, index) => sum / (index + 1))
  )
  .subscribe(console.log);

