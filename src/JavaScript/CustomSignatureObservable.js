//https://stackoverflow.com/questions/42674293/specify-signature-of-a-custom-rxjs-operator-in-typescript
import { Observable } from 'rxjs/Observable';

function restrictToCommand<T>(
  this: Observable<T>,
  cmd: string | string[]
): Observable<T> {

  return new Observable((observer) => {
    const obs = {
      next: (x) => {
        if (x.command === cmd || cmd.indexOf(x.command) !== -1) {
          observer.next(x);
        }
      },
      error: (err) => observer.error(err),
      complete: () => observer.complete()
    };
    return this.subscribe(obs);
  });
}

Observable.prototype.restrictToCommand = restrictToCommand;

declare module 'rxjs/Observable' {
  interface Observable<T> {
    restrictToCommand: typeof restrictToCommand;
  }
}
