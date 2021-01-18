import { TestBed, async, ComponentFixture, ComponentFixtureAutoDetect } from '@angular/core/testing';
import { BrowserModule, By } from "@angular/platform-browser";
import { GenreListComponent } from './genre-list.component';

let component: GenreListComponent;
let fixture: ComponentFixture<GenreListComponent>;

describe('genre-list component', () => {
    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [ GenreListComponent ],
            imports: [ BrowserModule ],
            providers: [
                { provide: ComponentFixtureAutoDetect, useValue: true }
            ]
        });
        fixture = TestBed.createComponent(GenreListComponent);
        component = fixture.componentInstance;
    }));

    it('should do something', async(() => {
        expect(true).toEqual(true);
    }));
});
