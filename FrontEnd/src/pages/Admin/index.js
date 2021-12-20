import React from 'react'
import './styles.css'
import { Link, Switch, Route, useRouteMatch } from "react-router-dom";
import { Box } from "@chakra-ui/react";
import Orders from './Orders'
import Products from './Products'
import Home from './Home'
import OrderDetail from './OrderDetail';
import ProductDetail from './ProductDetail';

function Admin() {
    const { path, url } = useRouteMatch();

	return (
		<div>
			<nav>
				<ul className="admin-menu">
					<li>
						<Link to={url}>Home</Link>
					</li>
					<li>
						<Link to={`${url}/orders`}>Orders</Link>
					</li>
					<li>
						<Link to={`${url}/products`}>Products</Link>
					</li>
				</ul>
			</nav>

			<Box mt="10">
				<Switch>
					<Route exact path={path} component={Home} />
					<Route exact path={`${path}/orders`} component={Orders} />
					<Route exact path={`${path}/products`} component={Products} />
					<Route path={`${path}/orders/:order_id`} component={OrderDetail}/>
					{/* <Route exact path={`${path}/products/new`} component={NewProduct} /> */}
					<Route path={`${path}/products/:product_id`} component={ProductDetail}/> 
				</Switch>
			</Box>
		</div>
	);
}

export default Admin;
