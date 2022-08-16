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

const del = async (url, body) =>
    fetch(url, {
            method: 'DELETE',
            body: JSON.stringify(body),
            headers: {
                'Content-Type': 'application/json'
            }
    });


export { post, get, del };