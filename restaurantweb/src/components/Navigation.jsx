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
    DocumentBulletListMultipleFilled,
    ShoppingBagFilled
} from "@fluentui/react-icons"

const useStyles = makeStyles({
    toolbar: {
        justifyContent: "space-between",
    },
});
function Navigation() {
    const farGroupStyles = useStyles();
    const navigate = useNavigate();
    return (
        <nav>
            <Toolbar
                className={farGroupStyles.toolbar}
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
                        onClick={() => navigate('/')}>
                        Home
                    </ToolbarButton>
                    <ToolbarButton
                        aria-label="Menu"
                        icon={<DocumentBulletListMultipleFilled />}
                        onClick={() => navigate('/menu')}>
                        New Order
                    </ToolbarButton>
                </ToolbarGroup>
                <ToolbarGroup>
                    <ToolbarButton
                        aria-label="shopping cart"
                        icon={<ShoppingBagFilled />}>
                        Shopping Cart
                    </ToolbarButton>
                </ToolbarGroup>
            </Toolbar>
        </nav>
    );
}

export default Navigation;
