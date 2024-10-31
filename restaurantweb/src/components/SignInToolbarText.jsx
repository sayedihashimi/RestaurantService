const signedInUser = ''
function SignInToolbarText() {
    const textToDisplay = !signedInUser ? "Sign In" : signedInUser.name;
    return (
        <>{textToDisplay}</>
    );
}

export default SignInToolbarText;