import React from "react";
import { useParams } from "react-router-dom";
import { useQuery } from "react-query";
import { fetchOrder } from "../../../api";
import {
  Table,
  Thead,
  Tbody,
  Tfoot,
  Tr,
  Th,
  Td,
  TableCaption,
} from '@chakra-ui/react'
  import moment from "moment";

function OrderDetail() {
  const { order_id } = useParams();
  const { isLoading, error, data } = useQuery(["admin:order", order_id], () =>
    fetchOrder(order_id)
  );

  console.log(data);

  if (isLoading) {
    return <div>Loading...</div>;
  }
  if (error) {
    return <div>Error</div>;
  }

  console.log(data);
  return (
    <Table variant='simple'>
  <TableCaption>Order Detail</TableCaption>
  <Thead>
    <Tr>
      <Th>Sipariş Numarası</Th>
      <Th>Toplam Tutar </Th>
      <Th>Teslimat Adresi</Th>
      <Th>Tahmini Teslimat Adresi</Th>
    </Tr>
  </Thead>
  <Tbody>
    <Tr>
      <Td fontWeight={750}>{data.orderNo}</Td>
      <Td fontWeight={750}> {data.orderTotal}</Td>
      <Td fontWeight={750}>{data.addressInfo.deliveryAddress}</Td>
      <Td fontWeight={750}>{moment(data.estimatedDeliveryDate).format("DD/MM/YYYY")}</Td>
    </Tr>
  </Tbody>
  <Tfoot>
    <Tr>
    <Th>Sipariş Numarası</Th>
      <Th>Toplam Tutar </Th>
      <Th>Teslimat Adresi</Th>
      <Th>Tahmini Teslimat Adresi</Th>
    </Tr>
  </Tfoot>
</Table>



    // <UnorderedList>
    //   <ListItem>     Sipariş Numarası :      {data.orderNo}</ListItem>
    //   <ListItem>      Toplam Tutar :           {data.orderTotal}</ListItem>
    //   <ListItem>Teslimat Adresi :{data.addressInfo.deliveryAddress}</ListItem>
    //   <ListItem>Tahmini Teslimat Bilgisi :{moment(data.estimatedDeliveryDate).format("DD/MM/YYYY")} </ListItem>
    // </UnorderedList>
  );
}

export default OrderDetail;
