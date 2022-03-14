import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Title } from '@angular/platform-browser';

@Injectable({
  providedIn: 'root'
})
export class AppService {
  private title = new BehaviorSubject<String>('Selamat datang');
  private title$ = this.title.asObservable();

  constructor(private browserTitle:Title) {}

  setTitle(title: String) {
    this.title.next(title);
    this.browserTitle.setTitle(title.toString());
  }

  getTitle(): Observable<String> {
    return this.title$;
  }
}