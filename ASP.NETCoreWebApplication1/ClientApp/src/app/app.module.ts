import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { DetailpageComponent } from './detailpage/detailpage.component';
import { EditGeneComponent } from './edit-gene/edit-gene.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    DetailpageComponent,
    EditGeneComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'detail/:id', component: DetailpageComponent, pathMatch: 'full' },
      { path: 'edit-gene/:id', component: EditGeneComponent, pathMatch: 'full' }
      
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
