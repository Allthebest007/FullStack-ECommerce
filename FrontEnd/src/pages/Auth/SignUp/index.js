import React from "react";
import {
  Flex,
  Box,
  Heading,
  FormControl,
  FormLabel,
  Input,
  Button,
  Text,
  Alert,
} from "@chakra-ui/react";
import { useFormik } from "formik";
import validationSchema from "./validations";
import {fetchRegister} from "../../../api"
import { useAuth } from "../../../contexts/AuthContext";
import { useHistory } from "react-router-dom";

function SignUp() {
    const history = useHistory();
    const {login} = useAuth()

  const formik = useFormik({
    initialValues: {
      email: "",
      password: "",
      username:"",
      passwordConfirm: "",
    },
    validationSchema,
    onSubmit: async (values, bag) => {
      try {
          const registerResponse = await fetchRegister({email:values.email, password:values.password, username : values.username});
          //login(registerResponse.data)
            let textElement = document.getElementById("showMessage");
            textElement.innerHTML = "You've successfully registered";
          setTimeout(() => {
            history.push("/signin");
          }, 3000);
          
      } catch (e) {
          bag.setErrors({general:e.response.data.errors.errors})
      }   
    },
  });

  return (
    <div>
      <Flex align="center" width="full" justifyContent="center">
        <Box pt={10}>

            <Text id="showMessage" color="tomato" >

            </Text>

          <Box textAlign="center">
            <Heading>Sign Up</Heading>
          </Box>
          
          <Box my={5}>
              {formik.errors.general && (
                <Alert status="error">
                    {formik.errors.general}
                </Alert>
                )}
          </Box>
          <Box my={5} textAlign="left">
            <form onSubmit={formik.handleSubmit}>
              <FormControl mt="4">
                <FormLabel>E-mail</FormLabel>
                <Input
                  name="email"
                  onChange={formik.handleChange}
                  onBlur={formik.handleBlur}
                  value={formik.values.email}
                  isInvalid={formik.touched.email && formik.errors.email}
                />
              </FormControl>
              <FormControl mt="4">
                <FormLabel>Username</FormLabel>
                <Input
                  name="username"
                  onChange={formik.handleChange}
                  onBlur={formik.handleBlur}
                  value={formik.values.username}
                  isInvalid={formik.touched.username && formik.errors.username}
                />
              </FormControl>
              <FormControl mt="4">
                <FormLabel>Password</FormLabel>
                <Input
                  name="password"
                  type="password"
                  onChange={formik.handleChange}
                  onBlur={formik.handleBlur}
                  value={formik.values.password}
                  isInvalid={formik.touched.password && formik.errors.password}
                />
              </FormControl>
              <FormControl mt="4">
                <FormLabel>Password Confirm</FormLabel>
                <Input
                  name="passwordConfirm"
                  type="password"
                  onChange={formik.handleChange}
                  onBlur={formik.handleBlur}
                  value={formik.values.passwordConfirm}
                  isInvalid={
                    formik.touched.passwordConfirm &&
                    formik.errors.passwordConfirm
                  }
                />
              </FormControl>
              <Button mt="4" width="full" type="submit">
                Sign Up
              </Button>
            </form>
          </Box>
        </Box>
      </Flex>
    </div>
  );
}

export default SignUp;
