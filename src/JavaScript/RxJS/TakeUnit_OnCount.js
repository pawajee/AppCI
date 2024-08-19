import {
  fromEvent,
  concatMap,
  switchMap,
  interval,
  take,
  takeWhile,
  takeUntil,
  empty,
  from,
  map,
  Subject,
} from 'rxjs';

const clicks = fromEvent(document, 'click');
//const clicks = fromEvent(document, 'click');
var stopNotification: any = empty(); //from([5]); //empty();
var destroy$ = new Subject<void>();
const result = clicks.pipe(
    switchMap((ev) => {
      console.log('doc clicked');
      return interval(1000); //.pipe(take(4))
    }),
    map((v) => {
      if (v < 5) {
        return v;
      }

      stopNotification = from([v]);
      destroy$.next();
      destroy$.complete();
      return v;
    }),
    takeUntil(destroy$)
  );
  
result.subscribe((x) => console.log(x));
