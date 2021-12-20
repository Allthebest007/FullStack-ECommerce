import React from 'react'
import {
    Alert,
    AlertIcon,
    AlertTitle,
    AlertDescription,
  } from '@chakra-ui/react'

function Error404() {
    return (
        <Alert status='error'>
  <AlertIcon />
  <AlertTitle mr={2}>ERROR 4 0 4 </AlertTitle>
  <AlertDescription>The page you are looking for does not FOUND !</AlertDescription>
  
</Alert>
    )
}

export default Error404
