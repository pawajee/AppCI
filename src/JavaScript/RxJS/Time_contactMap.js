import { of, timer } from 'rxjs';
import { concatMap, tap } from 'rxjs/operators';

// This could be any observable
const source = of(5, 7, 9);

timer(3000)
  .pipe(
    tap((_) => console.log(`befor concatMap:${_}`)),
    concatMap(() => source),
    tap((_) => console.log(`after concatMap:${_}`))
  )
  .subscribe(console.log);
