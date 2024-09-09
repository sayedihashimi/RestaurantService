const signedInUser = ''
function SignInToolbarText() {
    const textToDisplay = !signedInUser ? "Sign In" : signedInUser.name;
    return (
        <p>{textToDisplay}</p>
    );
}

export default SignInToolbarText;