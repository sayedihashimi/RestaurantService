import { useNavigate } from 'react-router-dom';
import './Navigation.css';
import { makeStyles } from "@fluentui/react-components";
import {
    Toolbar,
    ToolbarButton,
    ToolbarGroup,
} from "@fluentui/react-components";
import {
    HomeFilled,
    ShoppingBagFilled,
    ClipboardBulletListFilled,
    PersonCircleFilled,
    DocumentAddFilled
} from "@fluentui/react-icons"
import SignInToolbarText from './SignInToolbarText';

const useStyles = makeStyles({
    toolbar: {
        justifyContent: "space-between",
    },
    activePage: {
        color:'var(--third-color)',
        backgroundColor: 'var(--first-color)',
        ':hover': {
            backgroundColor: 'var(--first-color)',
            color: 'var(--third-color)',
        },
    },
});

function Navigation() {
    const navStyles = useStyles();
    const navigate = useNavigate();
    const currentPath = window.location.pathname;

    return (
        <nav>
            <Toolbar
                className={navStyles.toolbar}
                size="large"
                style={{
                    border: "2px solid black",
                    borderRadius: "0.5rem",
                    margin: "1rem"
                }}>
                <ToolbarGroup role="presentation">
                    <ToolbarButton
                        aria-label="Home"
                        icon={<HomeFilled />}
                        onClick={() => navigate('/')}
                        className={currentPath == '/' ? navStyles.activePage : ''}>
                            Home
                    </ToolbarButton>
                    <ToolbarButton
                        aria-label="Menu"
                        icon={<ClipboardBulletListFilled />}
                        onClick={() => navigate('/menu')}
                        className={currentPath == '/menu' ? navStyles.activePage : ''}>
                        Menu
                    </ToolbarButton>
                    <ToolbarButton
                        aria-label="New order"
                        icon={<DocumentAddFilled />}
                        onClick={() => navigate('/new-order')}
                        className={currentPath == '/new-order' ? navStyles.activePage : ''}>
                        New Order
                    </ToolbarButton>
                </ToolbarGroup>
                <ToolbarGroup>
                    <ToolbarButton
                        aria-label="shopping cart"
                        icon={<ShoppingBagFilled />}
                        className={currentPath == '/shopping-cart' ? navStyles.activePage : ''}>
                        Shopping Cart
                    </ToolbarButton>
                    <ToolbarButton
                        aria-label="signin"
                        icon={<PersonCircleFilled />}
                        onClick={()=>navigate('sign-in') }
                        className={currentPath == '/shopping-cart' ? navStyles.activePage : ''}>
                        <SignInToolbarText />
                    </ToolbarButton>
                </ToolbarGroup>
            </Toolbar>
        </nav>
    );
}

export default Navigation;
