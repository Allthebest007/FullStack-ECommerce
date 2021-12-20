import axios from 'axios';

const token = localStorage.getItem("access-token")
axios.interceptors.request.use(
    
	config => {
        config.headers.authorization = `Bearer ${token}`;
        return config;
    },
    error => {
        return Promise.reject(error);
    }
		
);

export const fetchProduct = async (id) => { 
    const data = await axios.get(`${process.env.REACT_APP_BASE_ENDPOINT}/product/${id}`)
    .then(res => {return (res.data.data)}   );
    //res.data.data
    return data;
};

export const fetchProductList = async() => {
    const { data } = await axios.get(`${process.env.REACT_APP_BASE_ENDPOINT}/product`)
    .then(res=>res.data);

    return data;
};

export const deleteProduct = async(product_id) => {
    const {data} = await axios.delete(`${process.env.REACT_APP_BASE_ENDPOINT}/product/${product_id}`)
    return data;
}

export const fetchCategoryList = async() => {
    const {data} = await axios.get(`${process.env.REACT_APP_BASE_ENDPOINT}/category`)
    .then(res=>res.data);
    return data;
}

export const postOrder = async(input) => {
    const {data} = await axios.post(`${process.env.REACT_APP_BASE_ENDPOINT}/order`,input)
    return data;
}

export const fetchRegister = async(input) => {
    const {data} = await axios.post(`${process.env.REACT_APP_BASE_ENDPOINT}/user`,input)
    return data;
}

export const fetchLogin = async(input) => {
    const {data} = await axios.post(`${process.env.REACT_APP_BASE_ENDPOINT}/auth/login`,input)
    return data;
}

export const fetchMe = async() => {
    const {data} = await axios.get(`${process.env.REACT_APP_BASE_ENDPOINT}/user`);
    
    return data;
}

export const fetchLogout = async() => {
    const {data} = await axios.post(`${process.env.REACT_APP_BASE_ENDPOINT}/auth/revokeRefreshToken`,{
        refreshToken : localStorage.getItem("refresh-token")
    })
    return data;
}

export const fetchOrders = async() => {
    const {data} = await axios.get(`${process.env.REACT_APP_BASE_ENDPOINT}/order`)
    .then(res=>res.data);
    
    return data;
}

export const fetchOrder = async (id) => { 
    const data = await axios.get(`${process.env.REACT_APP_BASE_ENDPOINT}/order/${id}`).
    then(res=>res.data.data);
    
    return data;
};

export const updateProduct = async (input, product_id) => {
	const { data } = await axios.put(
		`${process.env.REACT_APP_BASE_ENDPOINT}/product/${product_id}`,
		input
	);

	return data;
};
