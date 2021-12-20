import "./App.css";
import { BrowserRouter as Router, Switch, Route, } from "react-router-dom";
import Navbar from "./components/Navbar";
import Login from "./pages/Login";
import Products from './pages/Products';
import ProductDetail from './pages/ProductDetail';
import Basket from './pages/Basket'
import Error404 from "./pages/Error404";
import SignUp from "./pages/Auth/SignUp";
import SignIn from "./pages/Auth/SignIn";
import Profile from "./pages/Profile/index";
import ProtectedRoute from "./pages/ProtectedRoute";
import Admin from "./pages/Admin";
import Slider from "./components/Slider/Slider";

function App() {
  return (
    <Router>
      <div className="wrap">
        <Navbar />
        
      <br/>
      <br/>
      

        <div className="content">
        <Switch>
            <Route path="/" exact component={Products}/>
            <Route path="/login" component={Login}/>
            <Route path="/basket" component={Basket}/>
            <Route path="/signin" component={SignIn} />
						<Route path="/signup" component={SignUp} />
            <Route path="/product/:product_id" component={ProductDetail}/>
            <ProtectedRoute path="/profile" component={Profile}/>
            <ProtectedRoute path="/admin" component={Admin} admin={true}/>
            <Route path="*" component={Error404}/>
          </Switch>
        </div>
          {/* <div id="content">
          
        </div> */}
          
        
      </div>
    </Router>
  );
}





export default App;
