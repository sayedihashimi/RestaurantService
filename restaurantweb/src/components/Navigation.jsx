import { NavLink } from 'react-router-dom';
import './Navigation.css';
import { Link } from '@fluentui/react';
import { DefaultButton } from '@fluentui/react';

function Navigation() {
  return (
      <nav>
          <ul>
              <li><NavLink to='/' component={Link}>
                  <DefaultButton text="Home" /></NavLink></li>
              <li><NavLink to='/menu' component={Link}>
                  <DefaultButton text="Menu" /></NavLink></li>
              <li><NavLink to='/about' component={Link}>
                  <DefaultButton text="About" /></NavLink></li>
              <li><NavLink to='/contact' component={Link}>
                  <DefaultButton text="Contact" /></NavLink></li>
          </ul>
      </nav>


      // <nav>
      //    <ul>
      //        <li><NavLink to='/' component={Link}>Home</NavLink></li>
      //        <li><NavLink to='/menu' component={Link}>Menu</NavLink></li>
      //        <li><NavLink to='/about' component={Link}>About</NavLink></li>
      //        <li><NavLink to='/contact' component={Link}>Contact</NavLink></li>
      //    </ul>
      //</nav>
  );
}

export default Navigation;
