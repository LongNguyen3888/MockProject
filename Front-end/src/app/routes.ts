import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { WelcomeComponent } from './welcome/welcome.component';
import { LoginComponent } from './login/login.component';
import { AboutusComponent } from './aboutus/aboutus.component';
import { ContactComponent } from './contact/contact.component';
import { BlogviewComponent } from './blogview/blogview.component';
import { PostsComponent } from './posts/posts.component';

const routeConfig: Routes = [
    {
        path: '',
        redirectTo: 'welcome',
        pathMatch: 'full'
    },
    {
        path: 'welcome',
        component: WelcomeComponent,
        title: 'Welcome page'
    },
    {
        path: 'home',
        component: HomeComponent,
        title: 'Home page'
    },
    {
        path: 'login',
        component: LoginComponent,
        title: 'Login page',
        
    },
    {
        path: 'posts',
        component: PostsComponent,
        title: 'Posts page'
    },
    {
        path: 'blogview',
        component: BlogviewComponent,
        title: 'blogview page'
    },
    {
        path: 'contact',
        component: ContactComponent,
        title: 'Contact page'
    },
    {
        path: 'aboutus',
        component: AboutusComponent,
        title: 'Aboutus page'
    }
  ];
  
  export default routeConfig;