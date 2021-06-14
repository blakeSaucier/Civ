const post = async (url, body) => {
    return fetch(url, {
        method: 'POST',
        body: JSON.stringify(body),
        headers: {
            'Content-Type': 'application/json'
        }
    });
}

const get = async (url) => {
    return fetch(url, {
        method: 'GET'
    });
}

export { post, get };