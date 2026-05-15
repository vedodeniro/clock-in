import { Text, View, TextInput, TouchableOpacity, StyleSheet } from "react-native";
import { useState } from "react";

export default function HomeScreen() {
  const [employeeNumber, setEmployeeNumber] = useState("");

  return (
    <View style={styles.container}>
      <TextInput
        placeholder="Employee Number"
        value={employeeNumber}
        onChangeText={setEmployeeNumber}
        style={styles.textInput}
      />
      <TouchableOpacity style={styles.continueButton}>
        <Text style={styles.continueButtonText}>Continue</Text>
      </TouchableOpacity>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: "#fff",
    alignItems: "center",
    justifyContent: "center",
  },

  continueButton: {
    backgroundColor: "#007AFF",
    padding: 10,
    borderRadius: 5,
    marginTop: 20,
  },
  continueButtonText: {
    color: "#fff",
    fontSize: 16,
  },
  textInput: {
    borderWidth: 1,
    borderColor: "#ccc",
    padding: 10,
    width: "80%",
    borderRadius: 5,
  },
});
