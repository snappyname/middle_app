import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Login } from './components/login/login';
import { Register } from './components/register/register';
import { authRoutes } from './auth-routes.const';
import { GoogleAuthCallback } from './components/google-auth-callback/google-auth-callback';
import { AppConsts } from '../app.const';
import { GithubAuthCallback } from './components/github-auth-callback/github-auth-callback';

const routes: Routes = [
	{
		path: `${authRoutes.login}`,
		component: Login,
	},
	{
		path: `${authRoutes.register}`,
		component: Register,
	},
	{
		path: `${AppConsts.googleCallbackRoute}`,
		component: GoogleAuthCallback,
	},
	{
		path: `${AppConsts.githubCallbackRoute}`,
		component: GithubAuthCallback,
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class AuthRoutingModule {}
