import React from 'react'
import {
    Flex,
    Box,
    Heading,
    FormControl,
    FormLabel,
    Input,
    Button,
    Alert,
  } from "@chakra-ui/react";
  import { useFormik } from "formik";
  import validationSchema from "./validations";
  import {fetchLogin} from "../../../api"
import { useAuth } from '../../../contexts/AuthContext';

function SignIn({history}) {

    const {login} = useAuth();
    const formik = useFormik({
        initialValues: {
          email: "",
          password: "",
        },
        validationSchema,
        onSubmit: async (values, bag) => {
          try {
              const registerResponse = await fetchLogin(values);
              console.log(registerResponse.data);
              login(registerResponse.data)
              history.push("/profile")
          } catch (e) {
              bag.setErrors({general:e.response.data.errors.errors})
          }   
        },
      });

    return (
        <div>
      <Flex align="center" width="full" justifyContent="center">
        <Box pt={10}>

          <Box textAlign="center">
            <Heading>Login</Heading>
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
              
              <Button mt="4" width="full" type="submit">
                Login
              </Button>
            </form>
          </Box>
        </Box>
      </Flex>
    </div>
    )
}

export default SignIn
