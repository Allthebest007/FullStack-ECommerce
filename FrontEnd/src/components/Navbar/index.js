import React from "react";
import styles from "./styles.module.css";
import { Link } from "react-router-dom";
import { Button } from "@chakra-ui/react";
import { useBasket } from "../../contexts/BasketContext";
import { useAuth } from "../../contexts/AuthContext";
function Navbar() {

  const {loggedIn,user} = useAuth();
  const {items} = useBasket();

  return (
    <nav className={styles.nav}>
      <div className={styles.left}>
        <div className={styles.logo}>
          <Link to="/">InveonEcommerce</Link>
        </div>

        <ul className={styles.menu}>
          {/* <li>
            <Link to="/">Menu1</Link>
          </li> */}
        </ul>
      </div>

      {items.length > 0 && (
        <Link to="/basket">
          <Button colorScheme="pink" variant="outline">
            Basket ({items.length})
          </Button>
        </Link>
      ) }

      <div className={styles.right}>
        
        {!loggedIn && <>
          <Link to="/signin">
          <Button colorScheme="blue">Login</Button>
        </Link>
        <Link to="/signup">
          <Button colorScheme="blue">Register</Button>
        </Link>
        </>
      
        }

        {
          loggedIn && <>

          {user?.role === "Admin" && (
            <Link to="/admin">
                <Button colorScheme="pink" variant="ghost">
                  Admin
                </Button>
            </Link>
          )}
            <Link to="/profile">
          <Button colorScheme="green" >Profile</Button>
        </Link>
          </>
        }
        
        
        {/* */}
      </div>
    </nav>
  );
}

export default Navbar;
