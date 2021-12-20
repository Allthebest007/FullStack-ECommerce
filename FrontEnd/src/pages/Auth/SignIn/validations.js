import * as yup from "yup"

const validations = yup.object().shape({
	email: yup
		.string()
		.required("Zorunlu alan."),
	password: yup
		.string()
		.required("Zorunlu alan."),
    
});

export default validations;