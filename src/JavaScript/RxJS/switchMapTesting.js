import { timer, interval,from } from 'rxjs';
import { switchMap,tap } from 'rxjs/operators';

// switch to new inner observable when source emits, emit result of project function
//timer(1000,5000)

from([10,14,12])
.pipe(
  tap(_=>console.log(`tap0:${_}`)),
  switchMap(
    _ =>{ console.log(`switchmap:${_}`);return interval(2000)}

  ),
  tap(_=>console.log(`tap1:${_}`))
)
.subscribe(val => console.log(`ss:${val}`));

///////////////////////////////Original Example//////////////////////////////////
// RxJS v6+
import { timer, interval } from 'rxjs';
import { switchMap } from 'rxjs/operators';

// switch to new inner observable when source emits, emit result of project function
timer(0, 5000)
  .pipe(
    switchMap(
      _ => interval(2000),
      (outerValue, innerValue, outerIndex, innerIndex) => ({
        outerValue,
        innerValue,
        outerIndex,
        innerIndex
      })
    )
  )
  /*
	Output:
	{outerValue: 0, innerValue: 0, outerIndex: 0, innerIndex: 0}
	{outerValue: 0, innerValue: 1, outerIndex: 0, innerIndex: 1}
	{outerValue: 1, innerValue: 0, outerIndex: 1, innerIndex: 0}
	{outerValue: 1, innerValue: 1, outerIndex: 1, innerIndex: 1}
*/
  .subscribe(console.log);
